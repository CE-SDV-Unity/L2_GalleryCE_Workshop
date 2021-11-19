using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class museumAPI : MonoBehaviour
{
    public string wwwRequest = "https://api.vam.ac.uk/v2/objects/search?id_place=x28980&images_exist=1";
    public Material[] paintings_mat;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(museumRequest());
    }

    IEnumerator museumRequest()
    {
        yield return new WaitForSeconds(10);
        UnityWebRequest www = UnityWebRequest.Get(wwwRequest);

        yield return www.SendWebRequest();
        Debug.Log(www.downloadHandler.text);
        var response = JsonUtility.FromJson<museumva.Root>(www.downloadHandler.text);

        Debug.Log(response.info);
        Debug.Log(response.records);

        // foreach (var item in response.records)
        for (int i = 0; i < paintings_mat.Length; i++)
        {
            //string MediaUrl = "https://framemark.vam.ac.uk/collections/"+item._primaryImageId+"/full/!500,500/0/default.jpg";
            string MediaUrl = "https://framemark.vam.ac.uk/collections/" + response.records[i+20]._primaryImageId + "/full/!500,500/0/default.jpg";

            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log(request.error);
            else
               paintings_mat[i].mainTexture= ((DownloadHandlerTexture)request.downloadHandler).texture;

        }

    }



    public class museumva
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        [System.Serializable]
        public class Parameters
        {
        }

        [System.Serializable]
        public class Info
        {
            public string version;
            public int record_count;
            public bool record_count_exact;
            public Parameters parameters;
            public int page_size;
            public int pages;
            public int page;
            public int image_count;
        }

        [System.Serializable]
        public class Detail
        {
            public string free;
            public string @case;
            public string shelf;
            public string box;
        }

        [System.Serializable]
        public class CurrentLocation
        {
            public string id;
            public string displayName;
            public string type;
            public string site;
            public bool onDisplay;
            public Detail detail;
        }
        [System.Serializable]
        public class PrimaryMaker
        {
            public string name;
            public string association;
        }
        [System.Serializable]
        public class Images
        {
            public string _primary_thumbnail;
            public string _iiif_image_base_url;
            public string _iiif_presentation_url;
            public string imageResolution;
        }

        [System.Serializable]
        public class Record
        {
            public string systemNumber;
            public string accessionNumber;
            public string objectType;
            public CurrentLocation _currentLocation;
            public string _primaryTitle;
            public PrimaryMaker _primaryMaker;
            public string _primaryImageId;
            public string _primaryDate;
            public string _primaryPlace;
            public List<object> _warningTypes;
            public Images _images;
        }

        [System.Serializable]
        public class Term
        {
            public string id;
            public string value;
            public int count;
            public int count_max_error;
        }

        [System.Serializable]
        public class Category
        {
            public int other_terms_record_count;
            public List<Term> terms;
        }

        [System.Serializable]
        public class Person
        {
            public int other_terms_record_count;
            public List<Term> terms;
        }

        [System.Serializable]
        public class Organisation
        {
            public int other_terms_record_count;
            public List<Term> terms;
        }

        [System.Serializable]
        public class Collection
        {
            public int other_terms_record_count;
            public List<Term> terms;
        }

        [System.Serializable]
        public class Gallery
        {
            public int other_terms_record_count;
            public List<Term> terms;
        }

        [System.Serializable]
        public class Style
        {
            public int other_terms_record_count;
            public List<Term> terms;
        }

        [System.Serializable]
        public class Place
        {
            public int other_terms_record_count;
            public List<Term> terms;
        }

        [System.Serializable]
        public class ObjectType
        {
            public int other_terms_record_count;
            public List<Term> terms;
        }

        [System.Serializable]
        public class Technique
        {
            public int other_terms_record_count;
            public List<Term> terms;
        }

        [System.Serializable]
        public class Material
        {
            public int other_terms_record_count;
            public List<Term> terms;
        }

        [System.Serializable]
        public class Clusters
        {
            public Category category;
            public Person person;
            public Organisation organisation;
            public Collection collection;
            public Gallery gallery;
            public Style style;
            public Place place;
            public ObjectType object_type;
            public Technique technique;
            public Material material;
        }

        [System.Serializable]
        public class Root
        {
            public Info info;
            public List<Record> records;
            public Clusters clusters;
        }


    }







}


