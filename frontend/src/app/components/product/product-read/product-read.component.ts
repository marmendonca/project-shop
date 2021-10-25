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

  public products = []
  displayedColumns = ['id', 'name', 'price', 'category', 'action']
  public category: Category = undefined

  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    
    private route: ActivatedRoute) { }


  ngOnInit(): void {
    this.productService.read().subscribe(products => {
      this.products = products;

      this.products.forEach(product => {
        console.log(product)
        this.category = new Category()
        this.categoryService.readById(product.categoryId).subscribe((data: Category) => {
          this.category = data;
          product.categoryName = this.category.title;
        })
      });
    })
  }
}
