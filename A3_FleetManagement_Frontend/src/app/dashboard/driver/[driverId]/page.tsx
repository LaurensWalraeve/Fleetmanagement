"use client";

import "../../../globals.css";

import { Driver } from "@/app/components/forms/form-utils";
import { Card, CardTitle } from "@/app/components/ui/card";
import axios from "axios";
import { Input } from "../../../components/ui/input";
import { useEffect, useState } from "react";
import { UserNav } from "@/app/components/ui/user-nav";
import { MainNav } from "../../../components/ui/main-nav";
import { Search } from "@/app/components/ui/search";

const DriverDetailPage = (props: any) => {
  const [driver, setDriver] = useState<Driver | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const driverId = props.params.driverId;

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get<Driver>(`http://localhost:54315/api/Driver/${driverId}`);
        console.log("API Response:", response);
        setDriver(response.data);
        setLoading(false);
      } catch (error) {
        console.error("API Error:", error);
        setError("An error occurred while fetching driver data.");
        setLoading(false);
      }
    };

    if (!driverId) {
      console.error("DriverDetailPage: driverId is not defined");
      setError("Driver ID is not defined.");
      setLoading(false);
    } else {
      fetchData();
    }
  }, [driverId]);

  return (
    <>
    <div className="border-b">
      <div className="flex h-16 items-center px-4">
        <MainNav className="mx-6" />
        <div className="ml-auto flex items-center space-x-4">
        <Search />

          <UserNav />
        </div>
      </div>
      </div>

      <div className="flex-col md:flex pl-[5%] pt-[2%]">
        <div className="flex mb-[2%]">
          <Card className="grid gap-4 md:grid-cols-3 lg:grid-cols-2 p-4 mr-10 pl-10 ml-10">
        <div className="flex flex-col items-center">
              <CardTitle className="text-sm font-medium pr-[2%]">Driver detail</CardTitle>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">First Name:</span>
                <Input value={driver?.firstName} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Last Name:</span>
                <Input value={driver?.lastName} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Birth Date:</span>
                <Input value={driver?.birthDate} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">City:</span>
                <Input value={driver?.address.city} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">House Number:</span>
                <Input value={driver?.address.houseNumber} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Street:</span>
                <Input value={driver?.address.street} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Zipcode:</span>
                <Input value={driver?.address.zipCode} />
              </div>
            </div>
          </Card>

          <Card className="grid gap-4 md:grid-cols-3 lg:grid-cols-2 p-4 mr-10 pl-10 ml-10">
          <div className="flex flex-col items-center">
              <CardTitle className="text-sm font-medium pr-[2%]">Fuel Card detail</CardTitle>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Card Number:</span>
                <Input value={driver?.fuelCard.cardNumber} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Expiration Date:</span>
                <Input value={driver?.fuelCard.expirationDate?.toString()} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Pin Code:</span>
                <Input value={driver?.fuelCard.pinCode} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Blocked:</span>
                <Input value={driver?.fuelCard.blocked?.toString()} />
              </div>
            </div>
          </Card>

          <Card className="grid gap-4 md:grid-cols-3 lg:grid-cols-2 p-4 mr-10 pl-10 ml-10">
          <div className="flex flex-col items-center">
              <CardTitle className="text-sm font-medium pr-[2%]">Vehicle</CardTitle>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Vehicle:</span>
                <Input value={driver?.vehicle.chassisNumber} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Color:</span>
                <Input value={driver?.vehicle.color} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Fuel type:</span>
                <Input value={driver?.vehicle.fuelType.typeName} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">License plate:</span>
                <Input value={driver?.vehicle.licensePlate} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Make:</span>
                <Input value={driver?.vehicle.make} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Model:</span>
                <Input value={driver?.vehicle.model} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Number of doors:</span>
                <Input value={driver?.vehicle.numberOfDoors} />
              </div>
              <div className="mb-[2%]">
                <span className="text-sm font-medium">Vehicle type:</span>
                <Input value={driver?.vehicle.vehicleType.typeName} />
              </div>
            </div>
          </Card>
        </div>
      </div>
      </>
  );
};

export default DriverDetailPage;
