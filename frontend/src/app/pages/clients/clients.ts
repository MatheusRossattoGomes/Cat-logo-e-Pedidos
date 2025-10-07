import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { GridComponent } from '../../core/components/grid/grid';
import { AddButtonComponent } from '../../core/components/add-button/add-button';
import { EditButtonComponent } from '../../core/components/edit-button/edit-button';
import { DeleteButtonComponent } from '../../core/components/delete-button/delete-button';
import { ClientFormComponent } from './client-form/client-form';

@Component({
  selector: 'app-clients',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    GridComponent,
    AddButtonComponent,
    EditButtonComponent,
    DeleteButtonComponent
  ],
  templateUrl: './clients.html',
  styleUrls: ['./clients.css']
})
export class ClientsComponent {
  selectedItem: any | null = null;
  clientFields = ClientFormComponent.getFields();
}
