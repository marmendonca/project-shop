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

  public product: Product = undefined
  public categorys = [];
  public category: Category = undefined

  constructor(
    private productService: ProductService, 
    private router: Router, 
    private route: ActivatedRoute,
    private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.product = new Product();
    const id = +this.route.snapshot.paramMap.get('id')
    this.productService.readById(id).subscribe(product => {
      this.product = product
    });

    this.category = new Category()
    this.getCategoryList()

  }

  updateProduct(): void {
    this.productService.update(this.product).subscribe(() => {
      this.productService.showMessage('Produto atualizado com sucesso!');
      this.router.navigate(['/products']);

    })
  }
  selectChangeCategory(event: any){
    this.product.categoryId = parseInt(event.target.value)
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
