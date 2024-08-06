
function dateFromPerishable(jsonArray) {
    

    // Convert JSON string to JavaScript array
    var jsArray = JSON.parse(jsonArray);

    class DateSpl {
        constructor(dateStr) {
            const [month, day, year] = dateStr.split('/').map(Number);
            this.month = month;
            this.day = day;
            this.year = year;
        }
    }

    // Convert each date string to a DateSpl instance
    var dateObjects = jsArray.map(item => new DateSpl(item.Date));

    return (dateObjects);
}
