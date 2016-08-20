import { Component } from "@angular/core";
import { MenuItem } from "../models/menu-item.model";

export const MAIN_MENU_ITEMS: MenuItem[] = [
    { id: 1, label: "Backup Sources", route: "/backup-sources" },
    { id: 1, label: "Backup Destinations", route: "/backup-destinations" },
    { id: 1, label: "Backup Configurations", route: "/backup-configurations" }
];


@Component({
    selector: 'main-menu',
    template: `
<ul class="nav side-menu" style="">
    <li 
        *ngFor="let menuItem of menuItems" 
        (click)="onSelect(menuItem)"
        [class.selected]="menuItem === selectedMenuItem">
        <a href="#">{{menuItem.label}}</a>
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

    onSelect(menuItem: MenuItem) : void {
        alert(menuItem.label);
    }
}