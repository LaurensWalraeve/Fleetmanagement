"use client";
import "../../globals.css";

import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/app/components/ui/card";
import { Tabs, TabsContent } from "@/app/components/ui/tabs";
import { Search } from "@/app/components/ui/search";
import { UserNav } from "@/app/components/ui/user-nav";
import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import { MainNav } from "../../components/ui/main-nav";
import { DataTable } from "../../components/ui/data-table";
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
import { CreateFuelCardForm } from "@/app/components/forms/createfuelcard-from";
import {
  FuelCardFilter,
  fuelCardColumns,
} from "@/app/components/columns/fuelcard-column";

export default function FuelCardPage() {
  const [fuelCards, setFuelCards] = useState(null);
  const [fuelCardsCount, setFuelCardsCount] = useState(null);

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
    driver: z.object({
      lastName: z.string(),
      firstName: z.string(),
      birthDate: z.string(),
      socialSecurityNumber: z.number().int(),
      licenseType: z.string().nullable(),
      address: z.object({
        street: z.string(),
        houseNumber: z.string(),
        zipCode: z.string(),
        city: z.string(),
      }),
      vehicle: z.object({
        make: z.string(),
        model: z.string(),
        chassisNumber: z.string(),
        licensePlate: z.string(),
        color: z.string(),
      }),
      fuelCard: z.object({
        cardNumber: z.string(),
        expirationDate: z.string().nullable(),
        pinCode: z.string().nullable(),
        isBlocked: z.boolean(),
      }),
    }),
  });

  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      driver: {
        lastName: "",
        firstName: "",
        birthDate: "",
        socialSecurityNumber: 0,
        licenseType: null,
        address: {
          street: "",
          houseNumber: "",
          zipCode: "",
          city: "",
        },
        vehicle: {
          make: "",
          model: "",
          chassisNumber: "",
          licensePlate: "",
          color: "",
        },
        fuelCard: {
          cardNumber: "",
          expirationDate: null,
          pinCode: null,
          isBlocked: false,
        },
      },
    },
  });

  // Function to fetch data from the C# endpoints
  const fetchData = async () => {
    try {
      const response = await axios.get("http://localhost:54315/api/FuelCard");
      setFuelCards(response.data);
      setFuelCardsCount(response.data.length);
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
            <h2 className="text-3xl font-bold tracking-tight">Fuel Cards</h2>

            <div className="sflex items-center space-x-2">
              <Sheet>
                <SheetTrigger className="h-9 px-4 py-2 bg-primary text-primary-foreground shadow hover:bg-primary/90 inline-flex items-center justify-center rounded-md text-sm font-medium transition-colors focus-visible:outline-none focus-visible:ring-1 focus-visible:ring-ring disabled:pointer-events-none disabled:opacity-50">
                  Create Fuel Card
                </SheetTrigger>
                <SheetContent>
                  <SheetHeader>
                    <SheetTitle>Create Fuel Card Form</SheetTitle>
                    <SheetDescription>
                      <CreateFuelCardForm form={form} formSchema={formSchema} />
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
                      Total Fuel Cards
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
                      {fuelCardsCount ? fuelCardsCount : "UNKNOWN"}
                    </div>
                  </CardContent>
                </Card>
              </div>
              <div className="grid gap-4">
                <DataTable
                  data={fuelCards || []}
                  columns={fuelCardColumns}
                  FilterComponent={FuelCardFilter}
                />
              </div>
            </TabsContent>
          </Tabs>
        </div>
      </div>
    </>
  );
}
