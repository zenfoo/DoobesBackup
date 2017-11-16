import { Component } from "@angular/core";
import { MenuItem } from "../models/menu-item.model";

export const MAIN_MENU_ITEMS: MenuItem[] = [
    { id: 0, label: "Dashboard", route: "/dashboard" },
    { id: 1, label: "Sync configurations", route: "/syncconfigurations" }
];


@Component({
    selector: 'main-menu',
    template: `
<ul class="nav side-menu" style="">
    <li 
        *ngFor="let menuItem of menuItems" 
        (click)="onSelect(menuItem)"
        [class.selected]="menuItem === selectedMenuItem">
        <a routerLink="{{menuItem.route}}">{{menuItem.label}}</a>
    </li>
</ul>
`
})

export class MainMenuComponent {
    menuItems: MenuItem[] = MAIN_MENU_ITEMS;
    selectedMenuItem: MenuItem;

    constructor() {
        this.selectedMenuItem = this.menuItems[0];
    }

    onSelect(menuItem: MenuItem): void {
        this.selectedMenuItem = menuItem;
        //alert(menuItem.label);
        // TODO: store the last selected view
    }
}