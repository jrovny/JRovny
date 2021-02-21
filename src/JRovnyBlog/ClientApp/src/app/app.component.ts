import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, HostListener, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'JRovny';
  rightSidenavOpened = true;
  leftSidenavOpened = true;
  // innerWidth: number;
  xsScreen = false;
  smScreen = false;
  mdScreen = false;
  lgScreen = false;

  constructor(private breakpointObserver: BreakpointObserver) {
    this.breakpointObserver
      .observe([Breakpoints.XSmall])
      .subscribe((result) => (this.xsScreen = result.matches));
    this.breakpointObserver
      .observe([Breakpoints.Small])
      .subscribe((result) => (this.smScreen = result.matches));
    this.breakpointObserver
      .observe([Breakpoints.Medium])
      .subscribe((result) => (this.mdScreen = result.matches));
    this.breakpointObserver
      .observe([Breakpoints.Large, Breakpoints.XLarge])
      .subscribe((result) => (this.lgScreen = result.matches));
  }

  ngOnInit() {
    // this.innerWidth = window.innerWidth;
  }

  // @HostListener('window:resize', ['$event'])
  // onResize(event) {
  //   this.innerWidth = event.target.innerWidth;
  // }

  // xsScreen() {
  //   return this.innerWidth < 768;
  // }
  // smScreen() {
  //   return this.innerWidth >= 768;
  // }
  // mdScreen() {
  //   return this.innerWidth >= 992;
  // }
  // lgScreen() {
  //   return this.innerWidth >= 1200;
  // }
}
