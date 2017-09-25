using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schedule {

    private Airport departure;
    private Airport destination;
    private Airplane airplane;
    private InGameTime departureTime;

	public Schedule(Airport departure, Airport destination, Airplane airplane, InGameTime departureTime) {
        this.departure = departure;
        this.destination = destination;
        this.airplane = airplane;
        this.departureTime = departureTime;
    }

    public Airport getDeparture() {
        return this.departure;
    }

    public Airport getDestination() {
        return this.destination;
    }

    public Airplane getAirplane() {
        return this.airplane;
    }

    public InGameTime getDepartureTime() {
        return this.departureTime;
    }
}
