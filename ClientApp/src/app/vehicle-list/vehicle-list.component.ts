import { VehicleService } from './../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { Vehicle, KeyValuePair } from '../models/vehicle';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  vehicles: Vehicle[];
  // allVehicles: Vehicle[];
  makes: KeyValuePair[];
  query: any = {};
  // Render columns dynamically
  columns = [
    // Title: column to render
    // Key: what to send to the server
    // IsSortable: if this column is sortable
    { title: 'Id' },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
    { }
  ];

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes()
      .subscribe(makes => this.makes = makes);

    this.populateVehicles();

    // client filtering
    // this.vehicleService.getVehicles()
    //   .subscribe(vehicles => this.vehicles = this.allVehicles = vehicles);
  }

  private populateVehicles() {
    this.vehicleService.getVehicles(this.query)
      .subscribe(vehicles => this.vehicles = vehicles);
  }

  onFilterChange() {
    // server filtering
    this.populateVehicles();

    // client filtering
    // let vehicles = this.allVehicles;
    // if (this.filter.makeId) {
    //   vehicles = vehicles.filter(v => v.make.id == this.filter.makeId);
    // }
    // this.vehicles = vehicles;
  }

  resetFilter() {
    this.query = {};
    this.onFilterChange();
  }

  sortBy(columnName) {
    if (this.query.sortBy == columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }

    this.populateVehicles();
  }
}
