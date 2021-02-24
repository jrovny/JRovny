import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, OnInit, Input, HostListener } from '@angular/core';
import { Router } from '@angular/router';
import { PostSummary } from 'src/app/models/post-summary';

@Component({
  selector: 'app-post-summary',
  templateUrl: './post-summary.component.html',
  styleUrls: ['./post-summary.component.scss'],
})
export class PostSummaryComponent implements OnInit {
  @Input() post: PostSummary;
  xsScreen: boolean;

  constructor(
    private router: Router,
    private breakpointObserver: BreakpointObserver
  ) {
    this.breakpointObserver
      .observe([Breakpoints.XSmall])
      .subscribe((result) => (this.xsScreen = result.matches));
  }

  ngOnInit(): void {}

  openBlogPost() {
    // [routerLink]="['/posts/', post.slug]"
    this.router.navigate([`/posts/${this.post.slug}`]);
  }
}
