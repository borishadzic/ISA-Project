import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'range'
})
export class RangePipe implements PipeTransform {

  transform(value: number[], args?: number[]): any {
    if (!args)
      return value;
    var min = 0;
    var max = 0;
    if (args.length == 1) {
      max = Math.floor(args[0]);    
    }
    else if (args.length == 2) {
      min = Math.floor(args[0]);
      max = Math.floor(args[1]);
    }
    else
      return value;

    var retVal = new Array();

    for (var i = min; i < max; ++i)
      retVal.push(i);
    return retVal;
  }

}
