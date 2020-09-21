import { Component, OnInit } from "@angular/core";
import { AppService } from "./services/app.service";
import { Observable } from "rxjs";
import { PostSummary } from "./models/post-summary";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
  title = "JRovny Blog";
  blogPosts$: Observable<PostSummary[]>;

  constructor(private appService: AppService) {}

  ngOnInit() {
    this.blogPosts$ = this.appService.getBlogPostSummaries();
  }
}
