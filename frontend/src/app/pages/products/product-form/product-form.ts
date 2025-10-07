import { Component } from '@angular/core';
import { FormField } from '../../../core/components/form/form';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [],
  templateUrl: './product-form.html',
  styleUrls: ['./product-form.css']
})
export class ProductFormComponent {
  public static getFields(): FormField[] {
    return [
      { name: 'name', label: 'Nome', type: 'text', required: true },
      { name: 'sku', label: 'SKU', type: 'text', required: true },
      { name: 'price', label: 'Preço', type: 'number', required: true },
      { name: 'stockQty', label: 'Estoque', type: 'number', required: true },
      { name: 'isActive', label: 'Ativo', type: 'checkbox' }
    ];
  }
}