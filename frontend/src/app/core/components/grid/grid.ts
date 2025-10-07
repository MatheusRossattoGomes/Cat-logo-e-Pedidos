import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-grid',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './grid.html',
  styleUrls: ['./grid.css']
})
export class GridComponent implements OnInit {
  @Input() apiUrl: string = '';
  @Output() itemSelected = new EventEmitter<any | null>();

  data: any[] = [];
  columns: { field: string, header: string }[] = [];
  isLoading = true;
  selectedItem: any | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getData();
  }

  public getData(): void {
    this.isLoading = true;
    this.selectedItem = null;
    this.itemSelected.emit(null);
    this.http.get<any>(this.apiUrl).subscribe(response => {
      this.data = response.data;
      if (this.columns.length === 0 && this.data?.length > 0) {
        this.columns = Object.keys(this.data[0]).map(key => ({
          field: key,
          header: key.charAt(0).toUpperCase() + key.slice(1)
        }));
      }
      this.isLoading = false;
    });
  }

  selectItem(item: any): void {
    this.selectedItem = item;
    this.itemSelected.emit(item);
  }
}