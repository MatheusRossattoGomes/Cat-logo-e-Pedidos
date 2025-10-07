import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { GridComponent } from '../../core/components/grid/grid';
import { AddButtonComponent } from '../../core/components/add-button/add-button';
import { EditButtonComponent } from '../../core/components/edit-button/edit-button';
import { DeleteButtonComponent } from '../../core/components/delete-button/delete-button';
import { ProductFormComponent } from './product-form/product-form';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    GridComponent,
    AddButtonComponent,
    EditButtonComponent,
    DeleteButtonComponent
  ],
  templateUrl: './products.html',
  styleUrls: ['./products.css']
})
export class ProductsComponent {
  selectedItem: any | null = null;
  productFields = ProductFormComponent.getFields();
}