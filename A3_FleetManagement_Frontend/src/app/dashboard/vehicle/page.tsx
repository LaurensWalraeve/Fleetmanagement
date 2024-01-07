"use client";
import Image from "next/image";
import "../../globals.css";
import { Button } from "@/app/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/app/components/ui/card";
import { Tabs, TabsContent } from "@/app/components/ui/tabs";
import { CalendarDateRangePicker } from "@/app/components/ui/date-range-picker";
import { Overview } from "@/app/components/ui/overview";
import { RecentFuelCardUse } from "@/app/components/ui/recent-sales";
import { Search } from "@/app/components/ui/search";
import { UserNav } from "@/app/components/ui/user-nav";
import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import { DataTable } from "../../components/ui/data-table";
import { MainNav } from "../../components/ui/main-nav";
import {
  VehicleFilter,
  vehicleColumns,
} from "../../components/columns/vehicle-column";
import { CreateVehicleForm } from "../../components/forms/createvehicle-form";
import {
  Sheet,
  SheetContent,
  SheetDescription,
  SheetHeader,
  SheetTitle,
  SheetTrigger,
} from "../../components/ui/sheet";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import * as z from "zod";

export default function VehiclePage() {
  const [vehicles, setVehicles] = useState(null);
  const [vehiclesCount, setVehiclesCount] = useState(null);

  type Address = {
    addressId: number;
    street: string;
    houseNumber: string;
    zipCode: string;
    city: string;
  };

  type Vehicle = {
    make: string;
    model: string;
    chassisNumber: string;
    licensePlate: string;
    color: string;
  };

  type FuelCard = {
    cardNumber: string;
    expirationDate: string | null;
    pinCode: string | null;
    isBlocked: boolean;
  };

  type Driver = {
    lastName: string;
    firstName: string;
    birthDate: string;
    socialSecurityNumber: number;
    licenseType: string | null;
    address: Address;
    vehicle: Vehicle;
    fuelCard: FuelCard;
  };

  const formSchema = z.object({
    vehicle: z.object({
      make: z.string(),
      model: z.string(),
      chassisNumber: z.string(),
      licensePlate: z.string(),
      color: z.string(),
    }),
  });

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      vehicle: {
        make: "",
        model: "",
        chassisNumber: "",
        licensePlate: "",
        color: "",
      },
    },
  });

  // Function to fetch data from the C# endpoints
  const fetchData = async () => {
    try {
      const response = await axios.get("http://localhost:54315/api/Vehicle");
      setVehicles(response.data);
      setVehiclesCount(response.data.length);
    } catch (error) {
      console.error("Error fetching vehicle data:", error);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);
  return (
    <>
      <div className="hidden flex-col md:flex">
        <div className="border-b">
          <div className="flex h-16 items-center px-4">
            <MainNav className="mx-6" />
            <div className="ml-auto flex items-center space-x-4">
              <Search />
              <UserNav />
            </div>
          </div>
        </div>
        <div className="flex-1 space-y-4 p-8 pt-6">
          <div className="flex items-center justify-between space-y-2">
            <h2 className="text-3xl font-bold tracking-tight">Vehicles</h2>

            <div className="flex items-center space-x-2">
              <Sheet>
                <SheetTrigger className="h-9 px-4 py-2 bg-primary text-primary-foreground shadow hover:bg-primary/90 inline-flex items-center justify-center rounded-md text-sm font-medium transition-colors focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring disabled:pointer-events-none disabled:opacity-50">
                  Create Vehicle
                </SheetTrigger>
                <SheetContent>
                  <SheetHeader>
                    <SheetTitle>Create Vehicle Form</SheetTitle>
                    <SheetDescription>
                      <CreateVehicleForm form={form} formSchema={formSchema} />
                    </SheetDescription>
                  </SheetHeader>
                </SheetContent>
              </Sheet>
            </div>
          </div>
          <Tabs defaultValue="overview" className="space-y-4">
            <TabsContent value="overview" className="space-y-4">
              <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
                <Card>
                  <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
                    <CardTitle className="text-sm font-medium">
                      Total Vehicles
                    </CardTitle>
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      viewBox="0 0 24 24"
                      fill="none"
                      stroke="currentColor"
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth="2"
                      className="h-4 w-4 text-muted-foreground"
                    >
                      <rect width="20" height="14" x="2" y="5" rx="2" />
                      <path d="M2 10h20" />
                    </svg>
                  </CardHeader>
                  <CardContent>
                    <div className="text-2xl font-bold">
                      {vehiclesCount ? vehiclesCount : "UNKNOWN"}
                    </div>
                  </CardContent>
                </Card>
              </div>
              <div className="grid gap-4">
                <DataTable
                  data={vehicles || []}
                  columns={vehicleColumns}
                  FilterComponent={VehicleFilter}
                />
              </div>
            </TabsContent>
          </Tabs>
        </div>
      </div>
    </>
  );
}
