import { Component } from '@angular/core';
import { FormField } from '../../../core/components/form/form';

@Component({
  selector: 'app-client-form',
  standalone: true,
  imports: [],
  template: ``, // Este componente não tem template
  styles: []
})
export class ClientFormComponent {
  public static getFields(): FormField[] {
    return [
      { name: 'name', label: 'Nome', type: 'text', required: true },
      { name: 'email', label: 'Email', type: 'email', required: true },
      { name: 'document', label: 'Documento', type: 'text', required: true }
    ];
  }
}
