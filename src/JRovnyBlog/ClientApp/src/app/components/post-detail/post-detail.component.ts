import { Component, HostListener, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { PostDetail } from 'src/app/models/post-detail';
import { AppService } from 'src/app/services/app.service';
import { faFacebookF } from '@fortawesome/free-brands-svg-icons'
import { faTwitter } from '@fortawesome/free-brands-svg-icons'

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss']
})
export class PostDetailComponent implements OnInit {
  slug: any;
  post$: Observable<PostDetail>;
  facebookF = faFacebookF;
  faTwitter = faTwitter;
  innerWidth: any;

  constructor(private route: ActivatedRoute, private appService: AppService) { }

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.innerWidth = event.target.innerWidth;
  }

  ngOnInit(): void {
    this.innerWidth = window.innerWidth;
    this.route.params.subscribe(slug => { this.slug = slug.slug; });
    this.post$ = this.appService.getBlogPostBySlug(this.slug);
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
