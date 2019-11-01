import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common'

@Pipe({name: 'groupBy'})
export class GroupByPipe implements PipeTransform {
    transform(collection: any, property: string): any {
        // prevents the application from breaking if the array of objects doesn't exist yet
        if(!collection) {
            return null;
        }

        const groupedCollection = collection.reduce((previous, current)=> {
            if(!previous[current[property]]) {
                previous[current[property]] = [current];
            } else {
                previous[current[property]].push(current);
            }

            return previous;
        }, {});

        debugger
        // this will return an array of objects, each object containing a group of objects
       var result= Object.keys(groupedCollection).map(key => ({ key, value: groupedCollection[key] }));
console.log(result)
       return result;
    }
}
