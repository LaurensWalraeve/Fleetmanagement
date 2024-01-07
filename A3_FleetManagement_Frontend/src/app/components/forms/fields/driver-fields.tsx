import "@/app/globals.css";
import { Check, ChevronsUpDown } from "lucide-react";
import { cn } from "@/lib/utils";
import { Button } from "@/app/components/ui/button";
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
} from "@/app/components/ui/command";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/app/components/ui/popover";
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
import { Select } from "../../ui/select";
import { LicenseType } from "../form-utils";

export function DriverFields({ control }: { control: any }) {
  const [open, setOpen] = useState(false);
  const [selectedLicenses, setSelectedLicenses] = useState<number[]>([]); // Store array of licenseTypeIDs
  const [licenseTypes, setLicenseTypes] = useState<LicenseType[]>([]); // Use LicenseType for state

  // Fetch license types from API
  useEffect(() => {
    fetch("http://localhost:54315/api/Driver/LicenseTypes")
      .then((response) => response.json())
      .then((data: LicenseType[]) => setLicenseTypes(data)) // Ensure data is of type LicenseType[]
      .catch((error) => console.error("Error fetching license types:", error));
  }, []);

  // Handler for selecting/deselecting licenses
  const handleSelect = (licenseTypeID: number) => {
    // Specify type for licenseTypeID
    if (selectedLicenses.includes(licenseTypeID)) {
      setSelectedLicenses(
        selectedLicenses.filter((id) => id !== licenseTypeID)
      );
    } else {
      setSelectedLicenses([...selectedLicenses, licenseTypeID]);
    }
  };

  // Create label for the button based on selected licenses
  const selectedLabels = licenseTypes
    .filter((license) => selectedLicenses.includes(license.licenseTypeID))
    .map((license) => license.typeName)
    .join(", ");

  return (
    <>
      <FormField
        control={control}
        name="driver.lastName"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Last Name</FormLabel>
            <FormControl>
              <Input {...field} maxLength={50} />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name="driver.firstName"
        render={({ field }) => (
          <FormItem>
            <FormLabel>First Name</FormLabel>
            <FormControl>
              <Input {...field} maxLength={50} />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name="driver.birthDate"
        render={({ field }) => (
          <FormItem>
            <FormLabel>Date of Birth</FormLabel>
            <FormControl>
              <Input
                {...field}
                type="date"
                value={field.value || ""}
                onChange={(e) => field.onChange(e.target.value)}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name="driver.socialSecurityNumber"
        rules={{ required: "Social Security Number is required" }}
        render={({ field }) => (
          <FormItem>
            <FormLabel>Social Security Number</FormLabel>
            <FormControl className="relative">
              <Input
                {...field}
                className="ssn-format pl-12" // Adjust pl-* as needed to match the pseudo-element's content
                placeholder=" " // The actual placeholder is just a space
                value={field.value || ""}
                maxLength={11}
              />
            </FormControl>
            <FormMessage />
          </FormItem>
        )}
      />
      <FormField
        control={control}
        name="driver.licenseType"
        render={({ field }) => (
          <FormItem>
            <FormLabel>License Type</FormLabel>
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
                        : "Select licenses..."}
                      <ChevronsUpDown className="ml-2 h-4 w-4 shrink-0 opacity-50" />
                    </Button>
                  </PopoverTrigger>
                  <PopoverContent className="w-full p-0">
                    <Command>
                      <CommandGroup className="max-h-60 overflow-y-scroll">
                        {licenseTypes.map((license) => (
                          <CommandItem
                            key={license.licenseTypeID}
                            value={license.licenseTypeID.toString()}
                            onSelect={() => handleSelect(license.licenseTypeID)}
                          >
                            <Check
                              className={cn(
                                "mr-2 h-4 w-4",
                                selectedLicenses.includes(license.licenseTypeID)
                                  ? "opacity-100"
                                  : "opacity-0"
                              )}
                            />
                            {`${license.typeName} - ${license.description}`}
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
      ></FormField>
    </>
  );
}
