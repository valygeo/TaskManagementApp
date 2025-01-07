import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-projectstatus',
  templateUrl: './projectstatus.component.html',
  styleUrls: ['./projectstatus.component.css'],
})
export class ProjectstatusComponent implements OnInit {
  constructor() {}
  list = Array.from({ length: 20000 }).map((value, i) => 'Item #${i}');
  ngOnInit(): void {}
}
