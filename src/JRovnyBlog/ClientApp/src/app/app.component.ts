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

  /*
    xs (for phones - screens less than 768px wide)
    sm (for tablets - screens equal to or greater than 768px wide)
    md (for small laptops - screens equal to or greater than 992px wide)
    lg (for laptops and desktops - screens equal to or greater than 1200px wide)
  */

  constructor(private appService: AppService) { }

  ngOnInit() {
    this.innerWidth = window.innerWidth;
    this.blogPosts$ = this.appService.getBlogPostSummaries();
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
