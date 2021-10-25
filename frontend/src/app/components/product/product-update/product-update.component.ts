import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Category } from '../category.model';
import { CategoryService } from '../category.service';
import { Product } from '../product.model';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-update',
  templateUrl: './product-update.component.html',
  styleUrls: ['./product-update.component.css']
})
export class ProductUpdateComponent implements OnInit {

  product: Product;
  categorys: Category[];
  selectedCategory: any;
  category: Category = { }

  constructor(
    private productService: ProductService, 
    private router: Router, 
    private route: ActivatedRoute,
    private categoryService: CategoryService) { }

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id')
    this.productService.readById(id).subscribe(product => {
      this.product = product
    });

    this.categoryService.readById(this.product.categoryId);

  }

  updateProduct(): void {
    this.product.categoryId = parseInt(this.selectedCategory, 10);
    this.productService.update(this.product).subscribe(() => {
      this.productService.showMessage('Produto atualizado com sucesso!');
      this.router.navigate(['/products']);

    })
  }

  getCategoryById(id: number): any {
    this.categoryService.readById(id).subscribe(response => {
     this.category = response;
     console.log(this.category)
   })
 }

  getCategoryList(): any {
    this.categoryService.read().subscribe(response => {
      this.categorys = response;
    })
  }

  cancel(): void {
    this.router.navigate(['/products']);
  }

}
