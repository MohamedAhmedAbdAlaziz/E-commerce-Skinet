import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket, IBasketTotals } from '../../models/baskets';

@Component({
  selector: 'app-order-totals',
  templateUrl: './order-totals.component.html',
  styleUrls: ['./order-totals.component.scss'],
})
export class OrderTotalsComponent implements OnInit {
  basketTotal$: Observable<IBasketTotals>;

  subtotal: number;
  shippingPrice: number;
  total: number;

  constructor(private basketService: BasketService) {
    this.setValues();
  }

  ngOnInit(): void {
    this.setValues();
  }

  setValues() {
    this.basketTotal$ = this.basketService.basketTotal$;
    this.basketTotal$.subscribe((red) => {
      this.shippingPrice = red.shipping;
      this.subtotal = red.subtotal;
      this.total = red.total;
    });
  }
}
