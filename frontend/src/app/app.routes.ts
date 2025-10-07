import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home';
import { ProductsComponent } from './pages/products/products';
import { ClientsComponent } from './pages/clients/clients';
import { OrdersComponent } from './pages/orders/orders'; // Importa a nova lista
import { OrderFormComponent } from './pages/orders/order-form/order-form'; // Importa o formulário

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'clients', component: ClientsComponent },
  { path: 'orders', component: OrdersComponent }, // Rota principal de pedidos (lista)
  { path: 'orders/new', component: OrderFormComponent }, // Rota para criar novo pedido
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
