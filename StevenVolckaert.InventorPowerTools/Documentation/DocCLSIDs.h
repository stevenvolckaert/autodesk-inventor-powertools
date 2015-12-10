/*
  Copyright © 1998 by Autodesk, Inc.


  DESCRIPTION

  This file contains the definitions of CLSIDs that correspond to the various Document types
  produced by Inventor and its related products. The root storage of an Inventor file contains
  one of these embedded within it (and can be retrieved via the ReadClassStg Win32-API call).

  This file also contains the GUIDs associated with a 'sub-type' of the Document. The sub-type
  serves to further specialize a Document. The sub-type of a particular instance of a 
  Document can be retrieved as a property off the Document interface.


  HISTORY

  SS  :  06/17/99  :  Creation
*/

#ifndef _AUTODESK_INVENTOR_DOCCLSIDS_
#define _AUTODESK_INVENTOR_DOCCLSIDS_

#include <objbase.h>

/*-------------------- The Part Document -----------------------------------*/

// Part Document's CLSID

// {4D29B490-49B2-11D0-93C3-7E0706000000}
DEFINE_GUID(CLSID_InventorPartDocument, 
0x4D29B490, 0x49B2, 0x11d0, 0x93, 0xC3, 0x7E, 0x07, 0x06, 0x00, 0x00, 0x00);

#define CLSID_InventorPartDocument_RegGUID "4D29B490-49B2-11D0-93C3-7E0706000000"
#define CLSID_InventorPartDocument_Name L"Autodesk Inventor Part"

// Part Document SubTypes

// Sheet Metal
// {9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}
DEFINE_GUID(CLSID_InventorSheetMetalPart, 
0x9c464203, 0x9bae, 0x11d3, 0x8b, 0xad, 0x0, 0x60, 0xb0, 0xce, 0x6b, 0xb4);

#define CLSID_InventorSheetMetalPart_RegGUID "9C464203-9BAE-11D3-8BAD-0060B0CE6BB4"
#define CLSID_InventorSheetMetalPart_Name L"Autodesk Inventor Sheet Metal Part"

// Generic Proxy
// {92055419-B3FA-11D3-A479-00C04F6B9531}
DEFINE_GUID(CLSID_InventorGenericProxyPart, 
0x92055419, 0xb3fa, 0x11d3, 0xa4, 0x79, 0x0, 0xc0, 0x4f, 0x6b, 0x95, 0x31);

#define CLSID_InventorGenericProxyPart_RegGUID "92055419-B3FA-11D3-A479-00C04F6B9531"
#define CLSID_InventorGenericProxyPart_Name L"Autodesk Inventor Generic Proxy Part"

// Compatibility Proxy
// {9C464204-9BAE-11D3-8BAD-0060B0CE6BB4}
DEFINE_GUID(CLSID_InventorCompatibilityProxyPart, 
0x9c464204, 0x9bae, 0x11d3, 0x8b, 0xad, 0x0, 0x60, 0xb0, 0xce, 0x6b, 0xb4);

#define CLSID_InventorCompatibilityProxyPart_RegGUID "9C464204-9BAE-11D3-8BAD-0060B0CE6BB4"
#define CLSID_InventorCompatibilityProxyPart_Name L"Autodesk Inventor Compatibility Proxy Part"

// Catalog Proxy
// {9C88D3AF-C3EB-11D3-B79E-0060B0F159EF}
DEFINE_GUID(CLSID_InventorCatalogProxyPart, 
0x9c88d3af, 0xc3eb, 0x11d3, 0xb7, 0x9e, 0x0, 0x60, 0xb0, 0xf1, 0x59, 0xef);

#define CLSID_InventorCatalogProxyPart_RegGUID "9C88D3AF-C3EB-11D3-B79E-0060B0F159EF"
#define CLSID_InventorCatalogProxyPart_Name L"Autodesk Inventor Catalog Proxy Part"

// Molded Part
// {4D8D80D4-F5B0-4460-8CEA-4CD222684469}
DEFINE_GUID(CLSID_InventorMoldedPart, 
			0x4d8d80d4, 0xf5b0, 0x4460, 0x8c, 0xea, 0x4c, 0xd2, 0x22, 0x68, 0x44, 0x69);

#define CLSID_InventorMoldedPart_RegGUID "4D8D80D4-F5B0-4460-8CEA-4CD222684469"
#define CLSID_InventorMoldedPart_Name L"Autodesk Inventor Molded Part Document"


/*-------------------- The Assembly Document -----------------------------------*/

// The Assembly Document's CLSID

// {E60F81E1-49B3-11D0-93C3-7E0706000000}
DEFINE_GUID(CLSID_InventorAssemblyDocument, 
0xE60F81E1, 0x49B3, 0x11d0, 0x93, 0xC3, 0x7E, 0x07, 0x06, 0x00, 0x00, 0x00);

#define CLSID_InventorAssemblyDocument_RegGUID "E60F81E1-49B3-11D0-93C3-7E0706000000"
#define CLSID_InventorAssemblyDocument_Name L"Autodesk Inventor Assembly"

// Weldment
// {28EC8354-9024-440F-A8A2-0E0E55D635B0}
DEFINE_GUID(CLSID_InventorWeldment, 
0x28ec8354, 0x9024, 0x440f, 0xa8, 0xa2, 0xe, 0xe, 0x55, 0xd6, 0x35, 0xb0);

#define CLSID_InventorWeldment_RegGUID "28EC8354-9024-440F-A8A2-0E0E55D635B0"
#define CLSID_InventorWeldment_Name L"Autodesk Inventor Weldment"


/*-------------------- The Drawing Document -----------------------------------*/

// The Drawing Document's CLSID

// {BBF9FDF1-52DC-11D0-8C04-0800090BE8EC}
DEFINE_GUID(CLSID_InventorDrawingDocument, 
0xBBF9FDF1, 0x52DC, 0x11d0, 0x8C, 0x04, 0x08, 0x00, 0x09, 0x0B, 0xE8, 0xEC);

#define CLSID_InventorDrawingDocument_RegGUID "BBF9FDF1-52DC-11D0-8C04-0800090BE8EC"
#define CLSID_InventorDrawingDocument_Name L"Autodesk Inventor Drawing"


/*-------------------- The Design Element Document -----------------------------------*/

// The Design Element Document's CLSID

// {62FBB030-24C7-11D3-B78D-0060B0F159EF}
DEFINE_GUID(CLSID_InventorDesignElementDocument, 
0x62fbb030, 0x24c7, 0x11d3, 0xb7, 0x8d, 0x0, 0x60, 0xb0, 0xf1, 0x59, 0xef);

#define CLSID_InventorDesignElementDocument_RegGUID "62FBB030-24C7-11D3-B78D-0060B0F159EF"
#define CLSID_InventorDesignElementDocument_Name L"Autodesk Inventor iFeature"


/*-------------------- The Presentation Document -----------------------------------*/

// The Presentation Document's CLSID

// {76283A80-50DD-11D3-A7E3-00C04F79D7BC}
DEFINE_GUID(CLSID_InventorPresentationDocument, 
0x76283a80, 0x50dd, 0x11d3, 0xa7, 0xe3, 0x0, 0xc0, 0x4f, 0x79, 0xd7, 0xbc);

#define CLSID_InventorPresentationDocument_RegGUID "76283A80-50DD-11D3-A7E3-00C04F79D7BC"
#define CLSID_InventorPresentationDocument_Name L"Autodesk Inventor Presentation"

/*-------------------- The Design View Document -----------------------------------*/

// The Design View Document's CLSID

// {81B95C5D-8E31-4F65-9790-CCF6ECABD141}
DEFINE_GUID(CLSID_InventorDesignViewDocument,
0x81B95C5D, 0x8E31, 0x4F65, 0x97, 0x90, 0xCC, 0xF6, 0xEC, 0xAB, 0xD1, 0x41);

#define CLSID_InventorDesignViewDocument_RegGUID "81B95C5D-8E31-4F65-9790-CCF6ECABD141"
#define CLSID_InventorDesignViewDocument_Name L"Autodesk Inventor Design View"




#endif
