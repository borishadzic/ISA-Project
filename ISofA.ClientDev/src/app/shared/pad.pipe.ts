import { Pipe, PipeTransform } from '@angular/core';

export class PadPipeArgs {
  width: number;
  el?: string;
}

@Pipe({
  name: 'pad'
})
export class PadPipe implements PipeTransform {

  transform(value: any, args: PadPipeArgs): string {
    return this.pad(value || '', args.width, args.el);
  }

  pad(n, width, z?): string {
    z = z || '0';
    n = n + '';
    return n.length >= width ? n : new Array(width - n.length + 1).join(z) + n;
  }

}
