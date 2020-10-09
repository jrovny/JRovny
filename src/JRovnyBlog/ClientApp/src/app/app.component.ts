import { Component, HostListener, OnInit } from "@angular/core";
import { AppService } from "./services/app.service";
import { Observable } from "rxjs";
import { PostSummary } from "./models/post-summary";
import { environment } from 'src/environments/environment';

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
  title = "JRovny";
  rightSidenavOpened = true;
  leftSidenavOpened = true;
  innerWidth: number;
  baseUrl: string;

  constructor() { }

  ngOnInit() {
    this.innerWidth = window.innerWidth;
    this.baseUrl = environment.baseUrl;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.innerWidth = event.target.innerWidth;
  }

  xsScreen() {
    return this.innerWidth < 768;
  }

  smScreen() {
    return this.innerWidth >= 768;
  }

  mdScreen() {
    return this.innerWidth >= 992;
  }

  lgScreen() {
    return this.innerWidth >= 1200
  }
}
