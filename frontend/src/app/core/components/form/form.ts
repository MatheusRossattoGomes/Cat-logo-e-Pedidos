import { Component, Inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

export interface FormField {
  name: string;
  label: string;
  type: 'text' | 'number' | 'email' | 'checkbox';
  required?: boolean;
}

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [ CommonModule, ReactiveFormsModule, MatDialogModule, MatButtonModule ],
  templateUrl: './form.html',
  styleUrls: ['./form.css']
})
export class FormComponent implements OnInit {
  title: string;
  fields: FormField[];
  saveUrl: string;
  initialData: any;
  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    public dialogRef: MatDialogRef<FormComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.title = data.title;
    this.fields = data.fields;
    this.saveUrl = data.saveUrl;
    this.initialData = data.initialData;
  }

  ngOnInit(): void {
    const formControls: { [key: string]: any } = {};
    for (const field of this.fields) {
      const validators = field.required ? [Validators.required] : [];
      let defaultValue: any = '';
      if (field.type === 'number') { defaultValue = 0; }
      else if (field.type === 'checkbox') { defaultValue = false; }
      const value = this.initialData ? this.initialData[field.name] : defaultValue;
      formControls[field.name] = [value, validators];
    }
    this.form = this.fb.group(formControls);
  }

  onSave(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      alert('Por favor, preencha os campos obrigatórios.');
      return;
    }

    let saveOperation: Observable<any>;
    const formData = { ...this.form.value };

    // --- LÓGICA DE CONVERSÃO DE TIPOS ---
    for (const field of this.fields) {
      if (field.type === 'number' && formData[field.name] !== null) {
        formData[field.name] = parseFloat(formData[field.name]);
      }
      if (field.type === 'checkbox') {
        formData[field.name] = !!formData[field.name]; // Força a conversão para booleano
      }
    }

    if (this.initialData && this.initialData.id) {
      saveOperation = this.http.put(`${this.saveUrl}`, { ...formData, id: this.initialData.id });
    } else {
      saveOperation = this.http.post(this.saveUrl, formData);
    }

    saveOperation.subscribe({
      next: () => {
        alert('Dados salvos com sucesso!');
        this.dialogRef.close(true);
      },
      error: (err: HttpErrorResponse) => {
        console.error('Erro ao salvar:', err);
        const errorMsg = err.error?.errors ? JSON.stringify(err.error.errors) : err.message;
        alert(`Ocorreu um erro ao salvar os dados: ${errorMsg}`);
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }
}