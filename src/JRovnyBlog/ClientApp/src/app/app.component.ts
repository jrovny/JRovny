import { Component, HostListener, OnInit } from "@angular/core";
import { AppService } from "./services/app.service";
import { Observable } from "rxjs";
import { PostSummary } from "./models/post-summary";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
  title = "JRovny";
  blogPosts$: Observable<PostSummary[]>;

  rightSidenavOpened = true;
  leftSidenavOpened = true;
  innerWidth: number;

  constructor(private appService: AppService) { }

  ngOnInit() {
    this.innerWidth = window.innerWidth;
    this.setSidenavOpenedState();
    this.blogPosts$ = this.appService.getBlogPostSummaries();
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.innerWidth = event.target.innerWidth;
    this.setSidenavOpenedState();
  }

  private setSidenavOpenedState() {
    this.rightSidenavOpened = this.innerWidth >= 1024;
    this.leftSidenavOpened = this.innerWidth >= 1300;
  }
}
