import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phoneNumber'
})
export class PhoneNumberPipe implements PipeTransform {
  transform(value: string): string {
    if (!value) return value;

    // Remove all non-numeric characters
    let cleaned = ('' + value).replace(/\D/g, '');

    // Check if the cleaned number has the correct length
    let match = cleaned.match(/^(\d{3})(\d{3})(\d{4})$/);
    if (match) {
      return '(' + match[1] + ') ' + match[2] + '-' + match[3];
    }

    return value;
  }
}
