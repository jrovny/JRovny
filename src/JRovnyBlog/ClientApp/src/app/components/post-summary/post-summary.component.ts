import { Component, OnInit, Input } from "@angular/core";
import { PostSummary } from "src/app/models/post-summary";

@Component({
  selector: "app-post-summary",
  templateUrl: "./post-summary.component.html",
  styleUrls: ["./post-summary.component.scss"],
})
export class PostSummaryComponent implements OnInit {
  @Input() post: PostSummary;

  constructor() {}

  ngOnInit(): void {}
}
