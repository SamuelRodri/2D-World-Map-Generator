using System.Collections.Generic;

public class Feature
{
    public string type { get; set; }
    public Properties properties { get; set; }
    public Geometry geometry { get; set; }
}

public class Geometry
{
    public string type { get; set; }
    public List<List<List<float>>>? coordinates { get; set; } = null;
    public List<List<List<List<float>>>> Mcoordinates { get; set; }
}

public class Properties
{
    public int scalerank { get; set; }
    public string featurecla { get; set; }
    public int labelrank { get; set; }
    public string sovereignt { get; set; }
    public string sov_a3 { get; set; }
    public int adm0_dif { get; set; }
    public int level { get; set; }
    public string type { get; set; }
    public string admin { get; set; }
    public string adm0_a3 { get; set; }
    public int geou_dif { get; set; }
    public string geounit { get; set; }
    public string gu_a3 { get; set; }
    public int su_dif { get; set; }
    public string subunit { get; set; }
    public string su_a3 { get; set; }
    public int brk_diff { get; set; }
    public string name { get; set; }
    public string name_long { get; set; }
    public string brk_a3 { get; set; }
    public string brk_name { get; set; }
    public object brk_group { get; set; }
    public string abbrev { get; set; }
    public string postal { get; set; }
    public string formal_en { get; set; }
    public string formal_fr { get; set; }
    public string note_adm0 { get; set; }
    public string note_brk { get; set; }
    public string name_sort { get; set; }
    public string name_alt { get; set; }
    public int mapcolor7 { get; set; }
    public int mapcolor8 { get; set; }
    public int mapcolor9 { get; set; }
    public int mapcolor13 { get; set; }
    public int pop_est { get; set; }
    public double gdp_md_est { get; set; }
    public int pop_year { get; set; }
    public int lastcensus { get; set; }
    public int gdp_year { get; set; }
    public string economy { get; set; }
    public string income_grp { get; set; }
    public int wikipedia { get; set; }
    public object fips_10 { get; set; }
    public string iso_a2 { get; set; }
    public string iso_a3 { get; set; }
    public string iso_n3 { get; set; }
    public string un_a3 { get; set; }
    public string wb_a2 { get; set; }
    public string wb_a3 { get; set; }
    public int woe_id { get; set; }
    public string adm0_a3_is { get; set; }
    public string adm0_a3_us { get; set; }
    public int adm0_a3_un { get; set; }
    public int adm0_a3_wb { get; set; }
    public string continent { get; set; }
    public string region_un { get; set; }
    public string subregion { get; set; }
    public string region_wb { get; set; }
    public int name_len { get; set; }
    public int long_len { get; set; }
    public int abbrev_len { get; set; }
    public int tiny { get; set; }
    public int homepart { get; set; }
    public string filename { get; set; }
}

public class Root
{
    public string type { get; set; }
    public List<Feature> features { get; set; }
}