import { Component, OnInit, Input, HostListener } from "@angular/core";
import { Router } from '@angular/router';
import { PostSummary } from "src/app/models/post-summary";

@Component({
  selector: "app-post-summary",
  templateUrl: "./post-summary.component.html",
  styleUrls: ["./post-summary.component.scss"],
})
export class PostSummaryComponent implements OnInit {
  @Input() post: PostSummary;
  innerWidth: number;

  constructor(private router: Router) {
  }

  ngOnInit(): void {
    this.innerWidth = window.innerWidth;
  }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.innerWidth = event.target.innerWidth;
  }

  openBlogPost() {
    // [routerLink]="['/posts/', post.slug]"
    this.router.navigate([`/posts/${this.post.slug}`]);
  }
}
