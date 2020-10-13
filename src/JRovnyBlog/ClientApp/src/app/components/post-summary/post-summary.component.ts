import { Component, OnInit, Input, HostListener } from "@angular/core";
import { PostSummary } from "src/app/models/post-summary";

@Component({
  selector: "app-post-summary",
  templateUrl: "./post-summary.component.html",
  styleUrls: ["./post-summary.component.scss"],
})
export class PostSummaryComponent implements OnInit {
  @Input() post: PostSummary;
  innerWidth: number;

  constructor() {
  }

  ngOnInit(): void {
    this.innerWidth = window.innerWidth;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.innerWidth = event.target.innerWidth;
  }

  getImageAspectRatioPercentage() {
    if (this.post) {
      return this.post.height / this.post.width;
    } else {
      return 0;
    }
  }

  screenWidth(): number {
    return 1000;
  }
}
