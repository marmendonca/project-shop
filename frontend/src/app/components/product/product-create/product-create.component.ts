import { CategoryService } from './../category.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../product.model';
import { ProductService } from '../product.service';
import { Category } from '../category.model';


@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css']
})
export class ProductCreateComponent implements OnInit {

  product: Product = {
    title: '',
    price: null
  }
  public categorys = []

  constructor(
    private productService: ProductService, 
    private router: Router,
    private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.getCategoryList()
  }

  createProduct() {
    this.productService.create(this.product).subscribe(() => {
      this.productService.showMessage('Produto criado!')
      this.router.navigate(['/products'])
    })
  }

  selectChangeCategory(event: any) {
    this.product.categoryId = parseInt(event.target.value)
  }

  cancel(): void {
    this.router.navigate(['/products'])
  }

  getCategoryList(): any {
    this.categoryService.read().subscribe(response => {
      this.categorys = response;
    })
  }
}
