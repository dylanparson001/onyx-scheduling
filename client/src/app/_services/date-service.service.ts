import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateServiceService {

  constructor() { }
  returnDateTime(date: Date, time: string) {
    let timeArray = time.split(':');
    let monthString;
    let monthInt = date.getMonth() + 1;
    let dayInt = date.getDate();
    let dayString;

    if (dayInt < 10) {
      dayString = `0${dayInt}`;
    } else {
      dayString = `${dayInt}`;
    }

    if (monthInt < 10) {
      monthString = `0${monthInt}`;
    } else {
      monthString = `${monthInt}`;
    }

    let returnDate = new Date(`${date.getFullYear()}-${monthString}-${dayString}T${timeArray[0]}:${timeArray[1]}:00`
    );

    return returnDate;
  }
}
