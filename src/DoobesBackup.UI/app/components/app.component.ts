import { Component } from "@angular/core";

@Component({
    selector: 'my-app',
    template: `
<div class="row">
    <div class="col-md-3 left_col">
        <div class="left_col scroll-view">
            <div class="navbar nav_title" style="border: 0;">
                <a href="/" class="site_title"><i class="fa fa-paw"></i> <span>Doobes Backup</span></a>
            </div>
            <div class="clearfix"></div>

            <div id="sidebar-menu" class="main_menu_side hidden-print main_menu">
                <div class="menu_section active">
                    <h3>Main Menu</h3>
                    <main-menu></main-menu>
                </div>
            </div>
        </div>
    </div>

    <div class="top_nav">
        <div class="nav_menu">
            <nav class="" role="navigation">
                <div class="nav toggle">
                    <a id="menu_toggle"><i class="fa fa-bars"></i></a>
                </div>
            </nav>
        </div>
    </div>

    <div class="right_col" role="main">
        <h1>{{title}}</h1>
        <my-heroes></my-heroes>
    </div>
</div>

`
})

export class AppComponent {
    title: string = "Doobes Backup";
}