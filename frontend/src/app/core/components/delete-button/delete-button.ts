import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-delete-button',
  standalone: true,
  imports: [CommonModule],
  template: `<button class="btn-delete" (click)="deleteItem()" [disabled]="!item">Excluir</button>`,
  styles: [`.btn-delete { background-color: #e74c3c; color: white; border: none; padding: 8px 16px; border-radius: 6px; cursor: pointer; font-weight: 500; transition: background-color 0.2s; } .btn-delete:hover:not(:disabled) { background-color: #c0392b; } .btn-delete:disabled { background-color: #bdc3c7; cursor: not-allowed; }`]
})
export class DeleteButtonComponent {
  @Input() deleteUrl = '';
  @Input() item: any | null = null; 
  @Output() deleted = new EventEmitter<void>();

  constructor(private http: HttpClient) {}

  deleteItem(): void {
    if (!this.item || !this.deleteUrl) return;
    const id = this.item.id;
    if (!id) {
        console.error('Item selecionado não possui uma propriedade "id".');
        return;
    }

    if (window.confirm('Tem certeza que deseja excluir este item?')) {
      this.http.delete(`${this.deleteUrl}/${id}`).subscribe({
        next: () => {
          alert('Item excluído com sucesso!');
          this.deleted.emit();
        },
        error: (err) => {
          console.error('Erro ao excluir item', err);
          alert('Ocorreu um erro ao excluir o item.');
        }
      });
    }
  }
}