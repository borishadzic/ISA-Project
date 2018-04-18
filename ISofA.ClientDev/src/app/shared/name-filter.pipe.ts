import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'nameFilter',
  pure: false
})
export class NameFilterPipe implements PipeTransform {

  transform(values: any[], filter: string): any {
    if (!values || !filter)
      return values;
    return values.filter(x => (x.Name as string).toLowerCase().indexOf(filter.replace(/  +/g, ' ').trim()) !== -1);
  }

}
