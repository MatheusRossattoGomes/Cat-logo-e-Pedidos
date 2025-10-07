import { Component, Input, Output, EventEmitter } from '@angular/core';
import { DialogService } from '../../services/dialog.service';
@Component({
  selector: 'app-add-button',
  standalone: true,
  imports: [],
  template: `<button class="add-button" (click)="addItem()">Adicionar</button>`,
  styles: [`.add-button { background-color: #27ae60; color: white; border: none; padding: 6px 12px; border-radius: 6px; cursor: pointer; font-weight: 500; transition: background-color 0.2s; } .add-button:hover { background-color: #229954; }`]
})
export class AddButtonComponent {
  @Output() saved = new EventEmitter<void>();
  @Input() title: string = '';
  @Input() fields: any[] = [];
  @Input() saveUrl: string = '';

  constructor(private dialogService: DialogService) {}

  addItem() {
    const dialogRef = this.dialogService.openFormDialog({
      title: this.title,
      fields: this.fields,
      saveUrl: this.saveUrl
    });
    dialogRef.afterClosed().subscribe((result: any) => {
      if (result) {
        this.saved.emit();
      }
    });
  }
}
