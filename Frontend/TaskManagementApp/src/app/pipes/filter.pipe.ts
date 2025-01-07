import { Pipe, PipeTransform } from '@angular/core';
import { Project } from '../models/Project';

@Pipe({
  name: 'filter',
})
export class FilterPipe implements PipeTransform {
  // transform(value: any, searchTerm: any): any {

  //   if (searchTerm == '') {
  //     return value;
  //   }
  //   return value.filter(function (search) {
  //     return search.project_Name.toLowerCase().indexOf(searchTerm) > -1;
  //   });
  // }
  transform(value: any, searchText: any): any {
    if (!searchText) {
      return value;
    }
    return value.filter((data) => this.matchValue(data, searchText));
  }

  matchValue(data, value) {
    return Object.keys(data)
      .map((key) => {
        return new RegExp(value, 'gi').test(data[key]);
      })
      .some((result) => result);
  }
}
