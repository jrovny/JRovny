import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { PostSummary } from 'src/app/models/post-summary';
import { AppService } from 'src/app/services/app.service';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.scss']
})
export class PostListComponent implements OnInit {

  blogPosts$: Observable<PostSummary[]>;

  constructor(private appService: AppService) { }

  ngOnInit(): void {
    this.blogPosts$ = this.appService.getBlogPostSummaries();
  }

}
