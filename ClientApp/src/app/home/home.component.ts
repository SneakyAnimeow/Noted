import { Component } from '@angular/core';
import {LogInComponent} from "../log-in/log-in.component";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  protected readonly LogInComponent = LogInComponent;
}
