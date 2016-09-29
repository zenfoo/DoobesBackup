import { Component } from "@angular/core";

@Component({
    selector: 'doobes-app',
    template: `
<div class="row">
    <div class="col-md-3 left_col">
        <div class="scroll-view">
            <div class="navbar nav_title" style="border: 0;">
                <a routerLink="/" class="site_title"><h1>{{title}}</h1></a>
            </div>
            <div class="clearfix"></div>
            <div id="sidebar-menu" class="main_menu_side hidden-print main_menu">
                <div class="menu_section active">
                    <main-menu></main-menu>
                </div>
            </div>
        </div>
    </div>
    <div class="right_col col-md-9" role="main">
        <div class="top-nav">
            <div class="nav-menu">
                <nav class="" role="navigation">
                    <div class="nav toggle">
                        <a id="menu-toggle"><i class="fa fa-bars"></i></a>
                    </div>
                </nav>
            </div>
        </div>
        <router-outlet></router-outlet>
    </div>
</div>
`
})

export class AppComponent {
    title: string = "Doobes Backup";
}