import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { PostDetail } from 'src/app/models/post-detail';
import { faFacebookF } from '@fortawesome/free-brands-svg-icons';
import { faTwitter } from '@fortawesome/free-brands-svg-icons';
import { select, Store } from '@ngrx/store';
import { clearPost, loadPost } from 'src/app/store/actions/app.actions';
import { environment } from 'src/environments/environment';
import { Meta, Title } from '@angular/platform-browser';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.scss'],
})
export class PostDetailComponent implements OnInit, OnDestroy {
  slug: any;
  post$: Observable<PostDetail>;
  facebookF = faFacebookF;
  faTwitter = faTwitter;
  innerWidth: any;
  link: string;

  constructor(
    private route: ActivatedRoute,
    private store: Store<{ appState: { selectedPost: PostDetail } }>,
    private titleService: Title,
    private metaService: Meta
  ) {}

  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.innerWidth = event.target.innerWidth;
  }

  ngOnInit(): void {
    this.post$ = this.store.pipe(
      select((state) => state.appState.selectedPost)
    );
    this.innerWidth = window.innerWidth;
    this.route.params.subscribe((params) => {
      this.slug = params.slug;
      this.link = `${environment.baseUrl}/api/posts/${this.slug}`;
      this.store.dispatch(loadPost({ payload: this.slug }));
    });

    this.post$.subscribe((post) => {
      if (post) {
        this.titleService.setTitle(post.title);
        this.metaService.addTags([
          { name: 'og:url', content: this.link },
          { name: 'og:type', content: 'article' },
          { name: 'og:title', content: post.title },
          { name: 'og:description', content: post.title },
          { name: 'og:image', content: post.image },
        ]);
      }
    });
  }

  ngOnDestroy(): void {
    this.store.dispatch(clearPost());
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
    return this.innerWidth >= 1200;
  }
}
