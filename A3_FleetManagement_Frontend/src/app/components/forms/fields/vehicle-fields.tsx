import { useEffect, useState } from "react";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "../../ui/form";
import { Input } from "../../ui/input";
import { Vehicle } from "../form-utils";
import { Popover, PopoverContent, PopoverTrigger } from "../../ui/popover";
import { Button } from "../../ui/button";
import { Check, ChevronsUpDown } from "lucide-react";
import { Command, CommandGroup, CommandItem } from "../../ui/command";
import { cn } from "@/lib/utils";

export function VehicleFields({ control }: { control: any }) {
  const [open, setOpen] = useState(false);
  const [selectedVehicles, setSelectedVehicles] = useState<number[]>([]); // Store array of vehicleTypeIDs
  const [vehicles, setVehicles] = useState<Vehicle[]>([]); // Use VehicleType for state

  // Fetch vehicle types from API
  useEffect(() => {
    fetch("http://localhost:54315/api/Vehicle")
      .then((response) => response.json())
      .then((data: Vehicle[]) => setVehicles(data)) // Ensure data is of type VehicleType[]
      .catch((error) => console.error("Error fetching vehicle types:", error));
  }, []);

  // Handler for selecting/deselecting vehicles
  const handleSelect = (vehicleID: number) => {
    // Specify type for vehicleID
    if (selectedVehicles.includes(vehicleID)) {
      setSelectedVehicles(selectedVehicles.filter((id) => id !== vehicleID));
    } else {
      setSelectedVehicles([...selectedVehicles, vehicleID]);
    }
  };

  // Create label for the button based on selected licenses
  const selectedLabels = vehicles
    .filter((vehicle) => selectedVehicles.includes(vehicle.vehicleId))
    .map((vehicle) => vehicle.vehicleId)
    .join(", ");
  return (
    <>
      <FormField
        control={control}
        name="driver.vehicle.make"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Make</FormLabel>
            <FormControl>
              <Input {...field} />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name="driver.vehicle.model"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Model</FormLabel>
            <FormControl>
              <Input {...field} />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name="driver.vehicle.chassisNumber"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Chassis Number</FormLabel>
            <FormControl>
              <Input {...field} />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name="driver.vehicle.licensePlate"
        render={({ field }) => (
          <FormItem>
            <FormLabel>License Plate</FormLabel>
            <FormControl>
              <Input {...field} />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name="driver.vehicle.color"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Color</FormLabel>
            <FormControl>
              <Input {...field} />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name="driver.vehicles"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Existing Vehicles</FormLabel>
            <FormControl>
              <div className="w-full">
                <Popover open={open} onOpenChange={setOpen}>
                  <PopoverTrigger asChild>
                    <Button
                      variant="outline"
                      role="combobox"
                      aria-expanded={open}
                      className="w-full justify-between"
                    >
                      {selectedLabels.length > 0
                        ? selectedLabels
                        : "Select vehicles..."}
                      <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                    </Button>
                  </PopoverTrigger>
                  <PopoverContent className="w-full p-0">
                    <Command>
                      <CommandGroup className="max-h-60 overflow-y-scroll">
                        {vehicles.map((vehicle) => (
                          <CommandItem
                            key={vehicle.vehicleId}
                            value={vehicle.vehicleId.toString()}
                            onSelect={() => handleSelect(vehicle.vehicleId)}
                          >
                            <Check
                              className={cn(
                                "mr-2 h-4 w-4",
                                selectedVehicles.includes(vehicle.vehicleId)
                                  ? "opacity-100"
                                  : "opacity-0"
                              )}
                            />
                            {`${vehicle.vehicleId} - ${vehicle.vehicleType.typeName} - ${vehicle.licensePlate}`}
                          </CommandItem>
                        ))}
                      </CommandGroup>
                    </Command>
                  </PopoverContent>
                </Popover>
              </div>
            </FormControl>
          </FormItem>
        )}
      />
    </>
  );
}
