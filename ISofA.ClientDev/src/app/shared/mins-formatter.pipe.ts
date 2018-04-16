import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'minsFormatter'
})
export class MinsFormatterPipe implements PipeTransform {

  transform(value: number, args?: any): string {
    if (!value)
      return "00:00";
    return this.pad(Math.floor(value / 60), 2) + ":" + this.pad(value % 60, 2);
  }

  // number formatting
  pad(n, width, z?): string {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
  }

}
