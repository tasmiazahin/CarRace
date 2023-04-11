# CarRace
## Create an application where multiple car will be competing in separate thread (async/await)

The goals of the project is to create an application run different functions in different thread without blocking each others operation. User will be
able to check competation status by pressing any key during the competation. Since During the race multiple events will occure in every 30 seconds  such as refueling, tyer puncture, engine
failure etc., based on certain probability, I have calculated race details every 30 seconds and  prepare the status report.  


### Functionalities

* Cars run on separate thread
* Hands on working with async/await
* Status report
 
### Technology stack

* C#

### Challanges
Identifying how asynchronous operation works in general was challenging, further more  how CPU works with thread/thread pool.