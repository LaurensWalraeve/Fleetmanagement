"use client";

import { ColumnDef } from "@tanstack/react-table";
import { ArrowUpDown, MoreHorizontal } from "lucide-react";

import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "../ui/dropdown-menu";

import { Button } from "../ui/button";
import { Checkbox } from "../ui/checkbox";
import { Input } from "../ui/input";
import Link from "next/link";

type Address = {
  addressId: number;
  street: string;
  houseNumber: string;
  zipCode: string;
  city: string;
};

type Vehicle = {
  vehicleId: number;
  make: string;
  model: string;
  chassisNumber: string;
  licensePlate: string;
  color: string;
};

type FuelCard = {
  fuelCardId: number;
  cardNumber: string;
  expirationDate: string;
};

type Driver = {
  driverId: number;
  lastName: string;
  firstName: string;
  birthDate: string;
  socialSecurityNumber: number;
  licenseType: string | null;
  address: Address;
  vehicle: Vehicle;
  fuelCard: FuelCard;
};

type SortDirection = "asc" | "desc";

type ColumnType = {
  toggleSorting: (condition: boolean) => void;
  getIsSorted: () => false | SortDirection;
};

function createSortingHeader(column: ColumnType, title: string) {
  return (
    <Button
      className="text-left p-2"
      variant="ghost"
      onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
    >
      {title}
      <ArrowUpDown className="ml-2 h-4 w-4" />
    </Button>
  );
}

interface FilterProps {
  table: any; // You can replace 'any' with the appropriate type if available
}

export function DriverFilter({ table }: FilterProps) {
  return (
    // Your driver-specific filter logic and UI here
    <Input
      placeholder="Filter First Name..."
      value={(table.getColumn("firstName")?.getFilterValue() as string) ?? ""}
      onChange={(event) =>
        table.getColumn("firstName")?.setFilterValue(event.target.value)
      }
      className="max-w-sm"
    />
  );
}

export const driverColumns: ColumnDef<Driver>[] = [
  {
    id: "select",
    header: ({ table }) => (
      <Checkbox
        checked={table.getIsAllPageRowsSelected()}
        onCheckedChange={(value) => table.toggleAllPageRowsSelected(!!value)}
        aria-label="Select all"
      />
    ),
    cell: ({ row }) => (
      <Checkbox
        checked={row.getIsSelected()}
        onCheckedChange={(value) => row.toggleSelected(!!value)}
        aria-label="Select row"
      />
    ),
    enableSorting: false,
    enableHiding: false,
  },
  {
    accessorKey: "driverId",
    header: ({ column }) => createSortingHeader(column, "Driver ID"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("driverId")}</div>
    ),
  },
  {
    accessorKey: "firstName",
    header: ({ column }) => createSortingHeader(column, "First Name"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("firstName")}</div>
    ),
  },
  {
    accessorKey: "lastName",
    header: ({ column }) => createSortingHeader(column, "Last Name"),
    cell: ({ row }) => (
      <div className="text-left p-2">{row.getValue("lastName")}</div>
    ),
  },
  {
    accessorKey: "birthDate",
    header: ({ column }) => createSortingHeader(column, "Birth Date"),
    cell: ({ row }) => (
      <div className="text-left p-2">
        {new Date(row.getValue("birthDate")).toLocaleDateString()}
      </div>
    ),
  },
  {
    accessorKey: "address",
    header: ({ column }) => createSortingHeader(column, "Address"),
    cell: ({ row }) => {
      const address = row.getValue("address") as Address;
      if (
        address &&
        address.street &&
        address.houseNumber &&
        address.city &&
        address.zipCode
      ) {
        return (
          <div className="text-left p-2">{`${address.street}, ${address.houseNumber}, ${address.city}, ${address.zipCode}`}</div>
        );
      } else {
        return <div className="text-left p-2"></div>;
      }
    },
  },
  {
    accessorKey: "vehicle",
    header: ({ column }) => createSortingHeader(column, "Vehicle"),
    cell: ({ row }) => {
      const vehicle = row.getValue("vehicle") as Vehicle;
      if (vehicle && vehicle.make && vehicle.model && vehicle.licensePlate) {
        return (
          <div className="text-left p-2">{`${vehicle.make} ${vehicle.model} (${vehicle.licensePlate})`}</div>
        );
      } else {
        return <div className="text-left p-2"></div>;
      }
    },
  },
  {
    accessorKey: "fuelCard",
    header: ({ column }) => createSortingHeader(column, "Fuel Card"),
    cell: ({ row }) => {
      const fuelCard = row.getValue("fuelCard") as FuelCard;
      if (fuelCard && fuelCard.cardNumber && fuelCard.expirationDate) {
        return (
          <div className="text-left p-2">{`Card #: ${
            fuelCard.cardNumber
          } (Exp: ${new Date(
            fuelCard.expirationDate
          ).toLocaleDateString()})`}</div>
        );
      } else {
        return <div className="text-left p-2"></div>;
      }
    },
  },
  {
    id: "actions",
    cell: ({ row }) => {
      const driver = row.original;

      return (
        <div className="flex justify-end">
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="ghost" className="h-8 w-8 p-0">
                <span className="sr-only">Open menu</span>
                <MoreHorizontal className="h-4 w-4" />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuLabel>Actions</DropdownMenuLabel>
              <DropdownMenuItem
                onClick={() =>
                  navigator.clipboard.writeText(driver.driverId.toString())
                }
              >
                Copy driver ID
              </DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem>View driver</DropdownMenuItem>
              <DropdownMenuItem>
                <Link href={`/dashboard/driver/${driver.driverId}`}>
                  View driver details
                </Link>
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      );
    },
  },
];
