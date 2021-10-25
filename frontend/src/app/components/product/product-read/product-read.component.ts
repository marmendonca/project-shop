import { Category } from './../category.model';
import { Route, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Product } from '../product.model';
import { ProductService } from '../product.service';
import { CategoryService } from '../category.service';

@Component({
  selector: 'app-product-read',
  templateUrl: './product-read.component.html',
  styleUrls: ['./product-read.component.css']
})
export class ProductReadComponent implements OnInit {

  products: Product[]
  displayedColumns = ['id', 'name', 'price', 'category', 'action']
  category: Category = {  }

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.productService.read().subscribe(products => {
      this.products = products;
      this.products.forEach(product => {
        this.getCategoryById(product.categoryId);
        console.log(this.category)
      });
      console.log(products)
    })
  }

  getCategoryById(id: number): any {
     this.categoryService.readById(id).subscribe(response => {
      this.category = response;
      console.log(this.category)
    })
  }

}
