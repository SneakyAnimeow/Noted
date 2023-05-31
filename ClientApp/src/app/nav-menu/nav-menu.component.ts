import { Component } from '@angular/core';
import {LogInComponent} from "../log-in/log-in.component";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    if(!confirm("Are you sure you want to logout?")) {
      return;
    }

    console.log("logout");
    window.localStorage.removeItem("token");
    LogInComponent.isLogged = false;
    window.location.href = "/";
  }

  protected readonly LogInComponent = LogInComponent;
}
