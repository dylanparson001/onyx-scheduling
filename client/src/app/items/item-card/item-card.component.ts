import {Component, Input, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import {Item} from "../../models/item";

@Component({
  selector: 'app-item-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './item-card.component.html',
  styleUrl: './item-card.component.css'
})
export class ItemCardComponent implements OnInit{

  @Input() item: Item | undefined

  ngOnInit() {

  }

}
