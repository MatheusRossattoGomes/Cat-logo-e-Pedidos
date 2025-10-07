import { Component, Input, Output, EventEmitter } from '@angular/core';
import { DialogService } from '../../services/dialog.service';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-edit-button',
  standalone: true,
  imports: [],
  template: `<button class="btn-edit" (click)="editItem()" [disabled]="!item">Editar</button>`,
  styles: [`.btn-edit { background-color: #3498db; color: white; border: none; padding: 6px 12px; border-radius: 6px; cursor: pointer; font-weight: 500; transition: background-color 0.2s; } .btn-edit:hover:not(:disabled) { background-color: #2980b9; } .btn-edit:disabled { background-color: #bdc3c7; cursor: not-allowed; }`]
})
export class EditButtonComponent {
  @Output() saved = new EventEmitter<void>();
  @Input() item: any | null = null;
  @Input() title: string = '';
  @Input() fields: any[] = [];
  @Input() saveUrl: string = '';
  @Input() getUrl: string = '';

  constructor(private dialogService: DialogService, private http: HttpClient) {}

  editItem() {
    if (!this.item) return;
    this.http.get<any>(`${this.getUrl}/${this.item.id}`).subscribe(response => {
      const dialogRef = this.dialogService.openFormDialog({
        title: this.title,
        fields: this.fields,
        saveUrl: `${this.saveUrl}/${this.item.id}`,
        initialData: response.data
      });
      dialogRef.afterClosed().subscribe((result: any) => {
        if (result) {
          this.saved.emit();
        }
      });
    });
  }
}
