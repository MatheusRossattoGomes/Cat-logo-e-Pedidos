import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormComponent } from '../components/form/form';

@Injectable({
  providedIn: 'root'
})
export class DialogService {
  constructor(private dialog: MatDialog) {}

  openFormDialog(config: { title: string; fields: any[]; saveUrl: string; initialData?: any }): any {
    const dialogRef = this.dialog.open(FormComponent, {
      width: '500px',
      data: {
        title: config.title,
        fields: config.fields,
        saveUrl: config.saveUrl,
        initialData: config.initialData
      },
      disableClose: true
    });
    return dialogRef;
  }
}
