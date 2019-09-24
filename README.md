# Maerk.Sorting.System
Maerk.Sorting.System

It is a Maerk.Sorting.System project, which has been done as a test task
</br>
</br>
Current solution follows the specified technical task.</br>
1. JobWorker runs every 5 seconds, so please be patient.</br>
2. Duration in SortingJobDto is a double type, because most of the times it be so small, that it would be rounded to 0 if we use long.</br>
3. Unfortunately there are no tests, but there is an understanding that it is crusial to have them.</br>
4. There was a desire to create a solution with RabbitMq and MassTransit to call a background worker but considering lack of time it didn't happened.</br>