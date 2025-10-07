import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { debounceTime, switchMap, map } from 'rxjs/operators';

@Component({
  selector: 'app-order-form',
  standalone: true,
  imports: [CommonModule, RouterLink, ReactiveFormsModule],
  templateUrl: './order-form.html',
  styleUrls: ['./order-form.css']
})
export class OrderFormComponent implements OnInit {
  orderForm!: FormGroup;
  
  // Para o typeahead de clientes e produtos
  customerTypeahead$: Observable<any[]> = of([]);
  productTypeahead$: Observable<any[]> = of([]);

  constructor(private fb: FormBuilder, private http: HttpClient) {}

  ngOnInit(): void {
    this.orderForm = this.fb.group({
      customerSearch: [''],
      customerId: ['', Validators.required],
      productSearch: [''],
      totalAmount: [{ value: 0, disabled: true }],
      items: this.fb.array([], Validators.required)
    });

    this.setupCustomerTypeahead();
    this.setupProductTypeahead();
  }

  get items(): FormArray {
    return this.orderForm.get('items') as FormArray;
  }

  setupCustomerTypeahead(): void {
    this.customerTypeahead$ = this.orderForm.get('customerSearch')!.valueChanges.pipe(
      debounceTime(300),
      switchMap(value => this.http.get<any>(`/api/clients?search=${value}`)),
      map(response => response.data)
    );
  }

  setupProductTypeahead(): void {
    this.productTypeahead$ = this.orderForm.get('productSearch')!.valueChanges.pipe(
      debounceTime(300),
      switchMap(value => this.http.get<any>(`/api/products?search=${value}`)),
      map(response => response.data)
    );
  }

  selectCustomer(customer: any): void {
    this.orderForm.get('customerId')?.setValue(customer.id);
    this.orderForm.get('customerSearch')?.setValue(customer.name);
    this.customerTypeahead$ = of([]); // Limpa a lista
  }

  addProduct(product: any): void {
    const item = this.fb.group({
      productId: [product.id],
      productName: [product.name],
      quantity: [1, [Validators.required, Validators.min(1)]],
      unitPrice: [product.price],
      lineTotal: [product.price]
    });

    this.items.push(item);
    this.updateTotal();

    // Limpa a busca de produto
    this.orderForm.get('productSearch')?.setValue('');
    this.productTypeahead$ = of([]);
  }

  removeItem(index: number): void {
    this.items.removeAt(index);
    this.updateTotal();
  }

  updateLineTotal(index: number): void {
    const item = this.items.at(index);
    const quantity = item.get('quantity')?.value;
    const unitPrice = item.get('unitPrice')?.value;
    item.get('lineTotal')?.setValue(quantity * unitPrice);
    this.updateTotal();
  }

  updateTotal(): void {
    const total = this.items.controls.reduce((sum, control) => sum + control.get('lineTotal')?.value, 0);
    this.orderForm.get('totalAmount')?.setValue(total);
  }

  onSubmit(): void {
    if (this.orderForm.invalid) {
      alert('Formulário inválido. Verifique o cliente e os itens.');
      return;
    }
    
    // Lógica para enviar o pedido para o backend
    console.log('Pedido a ser enviado:', this.orderForm.getRawValue());
    alert('Funcionalidade de salvar pedido a ser implementada no backend.');
  }
}
